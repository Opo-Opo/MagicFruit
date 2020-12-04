_addon.name = "MagicFruit_addon"
_addon.author = "Opo-Opo"
_addon.version = "1"
_addon.description = "Inform Magic Fruit of Player and Party Events"
_addon.commands = {}

local port = 19766
local ip = "127.0.0.1"

local socket = require("socket")
require("packets")

function send(datagram)
    local connect = assert(socket.udp())
    assert(connect:sendto(datagram, ip, port))
    connect:close()
end

function incomingChunk(id, data)
    --if id == 0x63 or id == 0x76 or id == 0xDD or id == 0xDF then
        send(data)
    --end
end

windower.register_event("incoming chunk", incomingChunk)