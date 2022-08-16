
/********************************************************************
created:    2017-11-27
author:     lixianmin

*********************************************************************/

using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;

namespace Unicorn.UI
{
    internal static partial class UISerializerEditor
    {
        private static UISerializer.WidgetData _FillWidgetData (Transform root, string key, string name, string type)
        {
            var trans = _DeepFindWidget (root, name);
            if (null == trans)
            {
                return null;
            }

            var item = new UISerializer.WidgetData
            {
                key = key,
                name = name,
                type = type
            };

            if (type == "GameObject")
            {
                item.target = trans.gameObject;
            }
            else
            {
                var target = _GetWidgetTarget (trans, type);
                item.target = target;
                if (null != target)
                {
                    // if (target is UIText)
                    // {
                    //     var label = target as UIText;
                    //     if (!string.IsNullOrEmpty(label.GetGUID()))
                    //     {
                    //         item.userdata = label.GetGUID();
                    //     }
                    // }
                }
                else
                {
                    Console.Error.WriteLine("[_FillWidgetData()] Can not find a component with name={0}, type={1}", name, type);
                }
            }

            return item;
        }

        private static Component _GetWidgetTarget (Transform trans, string type)
        {
            var target = trans.GetComponent(type);
            if (null == target && type.StartsWith("UI."))
            {
                var script = trans.GetComponent(typeof(UISerializer));
                if (null != script)
                {
                    target = script;
                }
            }

            return target;
        }

        private static Transform _DeepFindWidget (Transform father, string name)
        {
            if (null != father && null != name)
            {
                foreach (var node in _ForEachNode(father))
                {
                    if (node.name == name)
                    {
                        return node;
                    }
                }
            }

            return null;
        }

        private static UISerializer.WidgetData _GetWidgetData (IList<UISerializer.WidgetData> dataList, string name, string type)
        {
            if (null != dataList)
            {
                var count = dataList.Count;
                for (int i = 0; i < count; i++)
                {
                    var item = dataList[i];
                    if (item.name == name && item.type == type)
                    {
                        return item;
                    }
                }
            }

            return null;
        }

        private static void _SavePrefab (Transform root)
        {
            EditorUtility.SetDirty(root.gameObject);
            AssetDatabase.SaveAssets();
        }

        private static string _SearchLuaScriptPath (string prefabName)
        {
            // var uiScriptRoot = os.path.join(UniqueManifest.GetLuaScriptRoot(), "UI");
            // var searchName = prefabName.Replace("_", string.Empty).ToLower() + ".lua";
            // Console.WriteLine("prefabName={0}, searchName={1}, uiScriptRoot={2}", prefabName, searchName, uiScriptRoot);
            //
            // foreach (var filepath in os.walk(uiScriptRoot, "*.lua"))
            // {
            //     var name = Path.GetFileName(filepath).ToLower();
            //     if (name.EndsWith(searchName))
            //     {
            //         return filepath;
            //     }
            // }

            return string.Empty;
        }

        private static string _CutCandidateText (string text)
        {
            var match = _GetHeadRegex().Match(text);
            var textLength = text.Length;
            var startIndex = match.Index + match.Length;
            var index = startIndex;

            var flag = 1;
            for (; index< textLength; ++index)
            {
                var c = text[index];
                if (c == '{')
                {
                    ++flag;
                }
                else if (c == '}')
                {
                    --flag;

                    if (flag == 0)
                    {
                        break;
                    }
                }
            }

            text = text.Substring(startIndex, index - startIndex);

            return text;
        }

        private static void _CollectWidgetFromLua (Transform root, IList<UISerializer.WidgetData> dataList)
        {
            var scriptPath = _SearchLuaScriptPath(root.name);
            if (!File.Exists(scriptPath))
            {
                return;
            }

            var text = File.ReadAllText(scriptPath);
            text = _CutCandidateText(text);

            const int validGroupCount = 5;

            foreach (Match match in _GetWidgetRegex().Matches(text))
            {
                var groups = match.Groups;
                if (groups.Count != validGroupCount)
                {
                    continue;
                }

                var key  = groups[2].Value;
                var name = groups[3].Value;
                var type = groups[4].Value;

                var lastData = _GetWidgetData(dataList, name, type);
                if (null != lastData)
                {
                    Console.Error.WriteLine("[_CollectWidgetFromLua()] Found an old widgetData with the same name={0}, type={1}", name, type);
                    continue;
                }

                var currentData = _FillWidgetData (root, key, name, type);
                if (null == currentData)
                {
                    Console.Error.WriteLine("[_CollectWidgetFromLua()] Can not find a widgetData with name = {0} ", name);
                    return;
                }

                _AddUniqueWidgetData(dataList, currentData);
            }
        }

        private static void _AddUniqueWidgetData (IList<UISerializer.WidgetData> dataList, UISerializer.WidgetData current)
        {
            if (null != dataList)
            {
                var count = dataList.Count;
                for (var i = 0; i < count; i++)
                {
                    var item = dataList[i];
                    if (item.target == current.target)
                    {
                        return;
                    }
                }

                dataList.Add(current);
            }
        }

        private static void _FetchLabels (UISerializer script)
        {
            var labelList = ListPool<Text>.Spawn();

            foreach (var node in _ForEachNode(script.transform))
            {
                var text = node.GetComponent<Text>();
                if (null != text)
                {
                    labelList.Add(text);
                }
            }

            script.labels = labelList.ToArray();
            ListPool<Text>.Recycle(labelList);
        }

//        [MenuItem("Assets/Test", false, 0)]
//        private static void _BroadcastControlMenu ()
//        {
//            var goSelected = Selection.activeObject as GameObject;
//            if (null == goSelected)
//            {
//                Console.Warning.WriteLine("Please select a prefab.");
//                return;
//            }
//
//            var sbText = new System.Text.StringBuilder();
//            foreach (var trans in _ForEachNode(goSelected.transform))
//            {
//                sbText.AppendLine(trans.name);
//            }
//
//            Console.WriteLine(sbText);
//        }

        private static IEnumerable<Transform> _ForEachNode (Transform father)
        {
            if (null == father) yield break;
            yield return father;

            var childCount = father.childCount;
            for (var i= 0; i< childCount; ++i)
            {
                var child = father.GetChild(i);
                var isNotControl = null == child.GetComponent(typeof(UISerializer));
                if (isNotControl)
                {
                    foreach (var node in _ForEachNode(child))
                    {
                        yield return node;
                    }
                }
                else
                {
                    yield return child;
                }
            }
        }

        private static void _CollectUITextWithGUID (UISerializer script, List<UISerializer.WidgetData> dataList)
        {
            var transform = script.transform;

            foreach (var item in script.labels)
            {
                var label = item as UIText1;
                if (null == label || string.IsNullOrEmpty(label.GetGUID()))
                {
                    continue;
                }

                var name = label.name;
                var type = "UIText";
                var lastData = _GetWidgetData(dataList, name, type);
                if (null != lastData)
                {
                    continue;
                }

                var currentData = _FillWidgetData(transform, name, name, type);
                if (null != currentData)
                {
                    _AddUniqueWidgetData(dataList, currentData);
                }
            }
        }

        private static void _CheckNameDuplication (Transform root)
        {
            var traversedTable = new Dictionary<string, Transform>();

            foreach (var child in _ForEachNode(root))
            {
                var name = child.name;
                if (name.Contains("(") || name.Contains(")"))
                {
                    var currentPath = child.GetFindPathEx();
                    Console.Error.WriteLine("\"()\" is not allowed in gameObject names, path={0}/{1}", root.name, currentPath);
                }

                var transLast = traversedTable.GetEx(name);
                if (null != transLast)
                {
                    string lastPath = transLast.GetFindPathEx();
                    string currentPath = child.GetFindPathEx();
                    Console.Error.WriteLine("Duplication name found: lastPath={0}/{1}\n, currentPath={0}/{2}", root.name, lastPath, currentPath);
                }
                else
                {
                    traversedTable.Add(name, child);
                }
            }
        }

        private static void _CheckEventSystemExistence (Transform transform)
        {
            var script = transform.GetComponentInChildren<UnityEngine.EventSystems.EventSystem>(true);
            if (null != script)
            {
                var path = script.transform.GetFindPathEx();
                Console.Warning.WriteLine("Caution: detect an EventSystem script ({0})", path);
            }
        }

        public static void SerializePrefab (UISerializer rootScript)
        {
            Console.WriteLine ("Begin serializing **********************");

            var root = rootScript.transform;

            _CheckNameDuplication(root);
            _CheckEventSystemExistence(root);

            rootScript.widgetDatas = null;
            var dataList = ListPool<UISerializer.WidgetData>.Spawn();
            _CollectWidgetFromLua(root, dataList);

            _FetchLabels(rootScript);
            _CollectUITextWithGUID(rootScript, dataList);

            rootScript.widgetDatas = dataList.ToArray();
            ListPool<UISerializer.WidgetData>.Recycle(dataList);

            _SavePrefab(root);

            Console.WriteLine ("End serializing **********************");
        }

        private static Regex _GetHeadRegex ()
        {
            if (null == _headRegex)
            {
                const string pattern = @"^\s*Layout\s*=\s*{";
                _headRegex = new Regex(pattern, RegexOptions.Multiline);
            }

            return _headRegex;
        }

        private static Regex _GetWidgetRegex ()
        {
            if (null == _widgetRegex)
            {
                var pattern = @"(^\s*(\w+)\s*=\s*\{\s*['""](\w+)['""]\s*,\s*['""](.+?)['""]\s*\}\s*[;,]?)+?";
                _widgetRegex = new Regex(pattern, RegexOptions.Multiline);
            }

            return _widgetRegex;
        }

        private static Regex _headRegex;
        private static Regex _widgetRegex;
    }
}