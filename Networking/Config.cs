using System;

namespace Networking
{
    public static class Config
    {
        /**DNS hostname of the masterserver */
        public const string NetworkMasterServerHost = "";

        /** DNS Hostname of the content server */
        public const string NetworkContentServerHost = "";
        /** DNS hostname of the HTTP-content mirror server */
        public const string NetworkContentMirrorHost = "";
        /** URL of the HTTP mirror system */
        public const string NetworkContentMirrorUrl = "/bananas"; 
        /** Message sent to the masterserver to 'identify' this client as OpenTTD */ 
        public const string NetworkMasterServerWelcomeMessage = "OpenTTDRegister";

        public const UInt16 NetworkMasterServerPort    = 3978;         // The default port of the master server (UDP)
        public const UInt16 NetworkContentServerPort   = 3978;         // The default port of the content server (TCP)
        public const UInt16 NetworkContentMirrorPort   =   80;         // The default port of the content mirror (TCP)
        public const UInt16 NetworkDefaultPort          = 3979;         // The default port of the game server (TCP & UDP)
        public const UInt16 NetworkAdminPort            = 3977;         // The default port for admin network
        public const UInt16 NetworkDefaultDebuglogPort = 3982;         // The default port debug-log is sent to (TCP)

        public const UInt16 SendMtu                      = 1460;         // Number of bytes we can pack in a single packet

        public const byte NetworkGameAdminVersion      =    1;         // What version of the admin network do we use?
        public const byte NetworkGameInfoVersion       =    4;         // What version of game-info do we use?
        public const byte NetworkCompanyInfoVersion    =    6;         // What version of company info is this?
        public const byte NetworkMasterServerVersion   =    2;         // What version of master-server-protocol do we use?

        public const uint NetworkNameLength             =   80;         // The maximum length of the server name and map name, in bytes including '\0'
        public const uint NetworkCompanyNameLength     =  128;         // The maximum length of the company name, in bytes including '\0'
        public const uint NetworkHostnameLength         =   80;         // The maximum length of the host name, in bytes including '\0'
        public const uint NetworkServerIdLength        =   33;         // The maximum length of the network id of the servers, in bytes including '\0'
        public const uint NetworkRevisionLength         =   33;         // The maximum length of the revision, in bytes including '\0'
        public const uint NetworkPasswordLength         =   33;         // The maximum length of the password, in bytes including '\0' (must be >= NETWORK_SERVER_ID_LENGTH)
        public const uint NetworkClientsLength          =  200;         // The maximum length for the list of clients that controls a company, in bytes including '\0'
        public const uint NetworkClientNameLength      =   25;         // The maximum length of a client's name, in bytes including '\0'
        public const uint NetworkRconcommandLength      =  500;         // The maximum length of a rconsole command, in bytes including '\0'
        public const uint NetworkGamescriptJsonLength  = SendMtu - 3; // The maximum length of a gamescript json string, in bytes including '\0'. Must not be longer than SEND_MTU including header (3 bytes)
        public const uint NetworkChatLength             =  900;         // The maximum length of a chat message, in bytes including '\0'

        public const uint NetworkGrfNameLength         =   80;         // Maximum length of the name of a GRF

        /**
         * Maximum number of GRFs that can be sent.
         * This limit is reached when PACKET_UDP_SERVER_RESPONSE reaches the maximum size of SEND_MTU bytes.
         */
        public const uint NetworkMaxGrfCount           =   62;

        public const uint NetworkNumLanguages           =   36; // Number of known languages (to the network protocol) + 1 for 'any'.

        /**
         * The number of landscapes in OpenTTD.
         * This number must be equal to NUM_LANDSCAPE, but as this number is used
         * within the network code and that the network code is shared with the
         * masterserver/updater, it has to be declared in here too. In network.cpp
         * there is a compile assertion to check that this NUM_LANDSCAPE is equal
         * to NETWORK_NUM_LANDSCAPES.
         */
        public const uint NetworkNumLandscapes          =    4;

    }
}