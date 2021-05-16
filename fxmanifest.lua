getmetatable "".__add = function(lhs, rhs) return lhs .. rhs; end; local ApplicationType = { Production = 1, Development = 2 };

local ENV = {
    ApplicationType = ApplicationType.Development
};

fx_version("cerulean");
game("gta5");

author("Sample");
description("A 100% custom framework for my awesome amazing server");
version("0.0.1a");

local appTypeFolder = "";
if ENV.ApplicationType == ApplicationType.Production then
    appTypeFolder = "Release";
elseif ENV.ApplicationType == ApplicationType.Development then
    appTypeFolder = "Debug";
end

client_script("./client.lua");

file("./images/*");

file("./Diamond.UserInterface/bin/Release/net5.0/publish/wwwroot/**");
ui_page("./Diamond.UserInterface/bin/Release/net5.0/publish/wwwroot/index.html");

client_script("./Diamond.Client/bin/" + appTypeFolder + "/MenuAPI.dll");
client_script("./Diamond.Client/bin/" + appTypeFolder + "/Diamond.Client.net.dll");
server_script("./Diamond.Server/bin/" + appTypeFolder + "/Diamond.Server.net.dll");


file("./Diamond.Client/bin/" + appTypeFolder + "/Newtonsoft.Json.dll");