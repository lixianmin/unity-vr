
-- Warning: all code of this file are generated automatically, so do not modify it manually ~
-- Any questions are welcome, mailto:lixianmin@gmail.com

local MetadataType = 
{
    cacheMetatable = { __mode = 'v' },
    readonlyMetatable =
    {
        __index = function (self, k)
            error (string.format('__index invalid global variables, [ %s => nil]', tostring(k)))
        end,

        __newindex = function (self, k, v)
            rawset(self, k, v)
            warning (string.format('__newindex unexpected global variables, [ %s => %s ]', tostring(k), tostring(v)))
        end,
    }

}
return MetadataType
