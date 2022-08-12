
-- Warning: all code of this file are generated automatically, so do not modify it manually ~
-- Any questions are welcome, mailto:lixianmin@gmail.com

local MetadataType = require 'Metadata.MetadataType'
local PetEatFishTemplate = { }
local cache = setmetatable({ }, MetadataType.cacheMetatable)

function PetEatFishTemplate._CreateStatUp (aid)
    aid.ReadUInt16 ()
    local item = {}
    item.stat_type = aid.ReadInt32 ()
    item.stat_value = aid.ReadSingle ()

    return setmetatable(item, MetadataType.readonlyMetatable)
end

function PetEatFishTemplate._CreatePetEatFishTemplate (aid)
    aid.ReadUInt16 ()
    local item = {}
    item.ages = { }
    aid.ReadByte()

    for index1 = 1, aid.ReadInt32() do
        item.ages[index1] = aid.ReadInt32 ()
    end
    item.another = { }
    aid.ReadByte()

    for index1 = 1, aid.ReadInt32() do
        item.another[index1] = aid.ReadInt32 ()
    end
    item.f32 = aid.ReadSingle ()
    item.height = aid.ReadInt32 ()
    item.hello = aid.ReadString ()
    item.i64 = aid.ReadInt64 ()
    item.id = aid.ReadInt32 ()
    item.locales = { }
    aid.ReadByte()

    for index1 = 1, aid.ReadInt32() do
        item.locales[index1] = aid.ReadLocaleText ()
    end
    item.normal_float = aid.ReadSingle ()
    item.stat_up = { }
    aid.ReadByte()

    for index1 = 1, aid.ReadInt32() do

        if aid.ReadBoolean() then
            item.stat_up[index1] = PetEatFishTemplate._CreateStatUp (aid)
        end
    end
    item.world = aid.ReadLocaleText ()

    return setmetatable(item, MetadataType.readonlyMetatable)
end

function PetEatFishTemplate.Create (idTemplate, ignoreError)
    local template = cache[idTemplate]
    if not template then
        local aid = Metadata.LuaLoadAid
        if aid.Seek ('Metadata.PetEatFishTemplate', idTemplate) and aid.ReadBoolean() then
            template = PetEatFishTemplate._CreatePetEatFishTemplate (aid)
        end

        if template then
            cache[idTemplate] = template
        elseif not ignoreError then
            error ('[PetEatFishTemplate.Create()] Invalid idTemplate= '..tostring(idTemplate))
        end
    end

    return template
end

function PetEatFishTemplate.GetIDs ()
    local ids = cache.ids
    if not ids then
        local aid = Metadata.LuaLoadAid
        ids = aid.EnumerateIDs ('Metadata.PetEatFishTemplate')
        cache.ids = ids
    end

    return ids
end

return PetEatFishTemplate
