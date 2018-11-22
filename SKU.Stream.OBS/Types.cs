using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace SKU.Stream.OBS
{
   public class Types
    {

    }
    public enum OutputState
    {
        /// <summary>
        /// The Output is initializing and doesn't produces frames yet
        /// </summary>
        Starting,
        /// <summary>
        /// The Output is running and produces frames
        /// </summary>
        Started,
        /// <summary>
        /// The output is stopping and sends the last remaining frames in its buffer
        /// </summary>
        Stopping,
        /// <summary>
        /// The output is completely stopped
        /// </summary>
        Stopped
    }
    /// <summary>
    /// Called by <see cref="ObsWebSocket.SceneChanged"/> 
    /// </summary>
    /// <param name="sender"><see cref="ObsWebSocket"/> instance</param>
    /// <param name="newSceneName">Name of the new current scene</param>
    public delegate void SceneChangeCallback(ObsWebSocket sender, string newSceneName);
    /// <summary>
    /// Called by <see cref="ObsWebSocket.SourceOrderChanged"/>
    /// </summary>
    /// <param name="sender"><see cref="ObsWebSocket"/> instance</param>
    /// <param name="sceneName">Name of the scene where items where preordered</param>
    public delegate void SourceOrderChangeCallback(ObsWebSocket sender, string sceneName);
    /// <summary>
    /// Called by <see cref="ObsWebSocket.SceneItemVisibilityChanged"/>, <see cref="ObsWebSocket.SceneItemAdded"/>
    /// or <see cref="ObsWebSocket.SceneItemRemoved"/>
    /// </summary>
    /// <param name="sender"><<see cref="ObsWebSocket"/> instance/param>
    /// <param name="sceneName">Name of the scene where the item is</param>
    /// <param name="itemName">Name of the concerned item</param>
    public delegate void SceneItemUpdateCallback(ObsWebSocket sender, string sceneName, string itemName);
    /// <summary>
    /// Called by <see cref="ObsWebSocket.TransitionChanged"/>
    /// </summary>
    /// <param name="sender"><see cref="ObsWebSocket"/> instance</param>
    /// <param name="newTransitionItem">Name of the new selected transition</param>
    public delegate void TransitionChangeCallback(ObsWebSocket sender, string newTransitionItem);
    /// <summary>
    /// Called by <see cref="ObsWebSocket.TransitionDurationChanged"/>
    /// </summary>
    /// <param name="sender"><see cref="ObsWebSocket"/> instance</param>
    /// <param name="newDuration">Name of the new transition duration (in milliseconds)</param>
    public delegate void TransitionDurationChangeCallback(ObsWebSocket sender, int newDuration);
    /// <summary>
    /// Called by <see cref="ObsWebSocket.StreamingStateChanged"/>, <see cref="ObsWebSocket.RecodingStateChanged"/>
    /// or <see cref="ObsWebSocket.ReplayBufferStateChanged"/>
    /// </summary>
    /// <param name="sender"><see cref="ObsWebSocket"/> instance</param>
    /// <param name="type">New output state</param>
    public delegate void OutputStateCallback(ObsWebSocket sender, OutputState type);
    /// <summary>
    /// Called by <see cref="ObsWebSocket.StreamStatus"/>
    /// </summary>
    /// <param name="sender"><see cref="ObsWebSocket"/> instance</param>
    /// <param name="status">Stream status data</param>
    public delegate void StreamStatusCallback(ObsWebSocket sender, StreamStatus status);
    /// <summary>
    /// Called by <see cref="ObsWebSocket.StudioModeSwitched"/>
    /// </summary>
    /// <param name="sender"><see cref="ObsWebSocket"/> instance</param>
    /// <param name="enabled">New Studio Mode status</param>
    public delegate void StudioModeChangedCallback(ObsWebSocket sender, bool enabled);
    /// <summary>
    /// Describes a scene in OBS, along with its items
    /// </summary>
    public struct OBSScene
    {
        /// <summary>
        /// OBS Scene name
        /// </summary>
        public string Name;
        /// <summary>
        /// Scene item list
        /// </summary>
        public List<SceneItem> Items;
        /// <summary>
        /// Builds the object from the JSON description
        /// </summary>
        /// <param name="data">JSON scene description as a <see cref="JObject"/></param>
        public OBSScene(JObject data)
        {
            Name = (string)data["name"];
            Items = new List<SceneItem>();

            var sceneItems = (JArray)data["sources"];
            foreach (JObject item in sceneItems)
            {
                Items.Add(new SceneItem(item));
            }
        }
    }
    /// <summary>
    /// Describes a scene item in an OBS scene
    /// </summary>
    public struct SceneItem
    {
        /// <summary>
        /// Source name
        /// </summary>
        public string SourceName;
        /// <summary>
        /// Source internal type
        /// </summary>
        public string InternalType;
        /// <summary>
        /// Source audio volume
        /// </summary>
        public float AudioVolume;
        /// <summary>
        /// Source item horizontal position/offset
        /// </summary>
        public float XPos;
        /// <summary>
        /// Source item vertical position/offset
        /// </summary>
        public float YPos;
        /// <summary>
        /// Item source width, without scaling and transforms applied
        /// </summary>
        public int SourceWidth;
        /// <summary>
        /// Item Source height, without scaling and transform applied
        /// </summary>
        public int SourceHeight;
        /// <summary>
        /// Item width
        /// </summary>
        public float Width;
        /// <summary>
        /// Item height
        /// </summary>
        public float Height;
        /// <summary>
        /// Builds the object from the JSON scene description
        /// </summary>
        /// <param name="data">JSON item description as a <see cref="JObject"/></param>
        public SceneItem(JObject data)
        {
            SourceName = (string)data["name"];
            InternalType = (string)data["type"];

            AudioVolume = (float)data["volume"];
            XPos = (float)data["x"];
            YPos = (float)data["y"];
            SourceWidth = (int)data["source_cx"];
            SourceHeight = (int)data["source_cy"];
            Width = (float)data["cx"];
            Height = (float)data["cy"];
        }
    }
    /// <summary>
    /// Data required by authentication
    /// </summary>
    public struct OBSAuthInfo
    {
        /// <summary>
        /// True if authentication is required, false otherwise
        /// </summary>
        public readonly bool AuthRequired;
        /// <summary>
        /// Authentication challenge
        /// </summary>
        public readonly string Challenge;
        /// <summary>
        /// Password salt
        /// </summary>
        public readonly string PasswordSalt;
        /// <summary>
        /// Builds the object from JSON response body
        /// </summary>
        /// <param name="data">JSON response body as a <see cref="JObject"/></param>
        public OBSAuthInfo(JObject data)
        {
            AuthRequired = (bool)data["authRequired"];
            Challenge = (string)data["challenge"];
            PasswordSalt = (string)data["salt"];
        }
    }
    /// <summary>
    /// Version info of the plugin, the API and OBS Studio
    /// </summary>
    public struct OBSVersion
    {
        /// <summary>
        /// obs-websocket plugin version
        /// </summary>
        public readonly string PluginVersion;
        /// <summary>
        /// OBS Studio version
        /// </summary>
        public readonly string OBSStudioVersion;
        /// <summary>
        /// Builds the object from JSON response body
        /// </summary>
        /// <param name="data">JSON response body as a <see cref="JObject"/></param>
        public OBSVersion(JObject data)
        {
            PluginVersion = (string)data["obs-websocket-version"];
            OBSStudioVersion = (string)data["obs-studio-version"];
        }
    }
    /// <summary>
    /// Data of the stream status update
    /// </summary>
    public struct StreamStatus
    {
        /// <summary>
        /// True if streaming is started and running, false otherwise
        /// </summary>
        public readonly bool Streaming;
        /// <summary>
        /// True if recording is started and running, false otherwise
        /// </summary>
        public readonly bool Recording;
        /// <summary>
        /// Stream bitrate in bytes per second
        /// </summary>
        public readonly int BytesPerSec;
        /// <summary>
        /// Stream bitrate in kilobits per second
        /// </summary>
        public readonly int KbitsPerSec;
        /// <summary>
        /// RTMP output strain
        /// </summary>
        public readonly float Strain;
        /// <summary>
        ///  Total time since streaming start
        /// </summary>
        public readonly int TotalStreamTime;
        /// <summary>
        /// Number of frames sent since streaming start
        /// </summary>
        public readonly int TotalFrames;
        /// <summary>
        /// Overall number of frames dropped since streaming start
        /// </summary>
        public readonly int DroppedFrames;
        /// <summary>
        /// Current framerate in Frames Per Second
        /// </summary>
        public readonly float FPS;
        /// <summary>
        /// Builds the object from the JSON event body
        /// </summary>
        /// <param name="data">JSON event body as a <see cref="JObject"/></param>
        public StreamStatus(JObject data)
        {
            Streaming = (bool)data["streaming"];
            Recording = (bool)data["recording"];

            BytesPerSec = (int)data["bytes-per-sec"];
            KbitsPerSec = (int)data["kbits-per-sec"];
            Strain = (float)data["strain"];
            TotalStreamTime = (int)data["total-stream-time"];

            TotalFrames = (int)data["num-total-frames"];
            DroppedFrames = (int)data["num-dropped-frames"];
            FPS = (float)data["fps"];
        }
    }
    /// <summary>
    /// Status of streaming output and recording output
    /// </summary>
    public struct OutputStatus
    {
        /// <summary>
        /// True if streaming is started and running, false otherwise
        /// </summary>
        public readonly bool IsStreaming;
        /// <summary>
        /// True if recording is started and running, false otherwise
        /// </summary>
        public readonly bool IsRecording;
        /// <summary>
        /// Builds the object from the JSON response body
        /// </summary>
        /// <param name="data">JSON response body as a <see cref="JObject"/></param>
        public OutputStatus(JObject data)
        {
            IsStreaming = (bool)data["streaming"];
            IsRecording = (bool)data["recording"];
        }
    }
    /// <summary>
    /// Current transition settings
    /// </summary>
    public struct TransitionSettings
    {
        /// <summary>
        /// Transition Name
        /// </summary>
        public readonly string Name;
        /// <summary>
        /// Transition duration in milliseconds
        /// </summary>
        public readonly int Duration;
        /// <summary>
        /// Builds the object from the JSON response body
        /// </summary>
        /// <param name="data">JSON response body as a <see cref="JObject"/></param>
        public TransitionSettings(JObject data)
        {
            Name = (string)data["name"];
            Duration = (int)data["duration"];
        }
    }
    /// <summary>
    /// Volume settings of an OBS source
    /// </summary>
    public struct VolumeInfo
    {
        /// <summary>
        /// Source Volume in linear scale (0.0 to 1.0)
        /// </summary>
        public readonly float Volume;
        /// <summary>
        /// True if source is muted, false otherwise
        /// </summary>
        public readonly bool Muted;
        /// <summary>
        /// Builds the object from the JSON response body
        /// </summary>
        /// <param name="data">JSON response body as a <see cref="JObject"/></param>
        public VolumeInfo(JObject data)
        {
            Volume = (float)data["volume"];
            Muted = (bool)data["muted"];
        }
    }
    /// <summary>
    /// Streaming settings
    /// </summary>
    public struct StreamingService
    {
        /// <summary>
        /// Type of streaming service
        /// </summary>
        public string Type;
        /// <summary>
        /// Streaming service settings (JSON data)
        /// </summary>
        public JObject Settings;
    }
    /// <summary>
    /// Common RTMP settings (predefined streaming service list
    /// </summary>
    public struct CommonRTMPStreamingService
    {
        /// <summary>
        /// Streaming provider name
        /// </summary>
        public string ServiceName;
        /// <summary>
        /// Streaming server URL;
        /// </summary>
        public string ServerUrl;
        /// <summary>
        /// Stream Key
        /// </summary>
        public string StreamKey;
        /// <summary>
        /// Construct object from data provided by <see cref="StreamingService.Settings"/>
        /// </summary>
        /// <param name="settings"></param>
        public CommonRTMPStreamingService(JObject settings)
        {
            ServiceName = (string)settings["service"];
            ServerUrl = (string)settings["server"];
            StreamKey = (string)settings["key"];
        }
        /// <summary>
        /// Convert to JSON object
        /// </summary>
        /// <returns></returns>
        public JObject ToJSON()
        {
            var obj = new JObject();
            obj.Add("service", ServiceName);
            obj.Add("server", ServerUrl);
            obj.Add("key", StreamKey);
            return obj;
        }
    }
    /// <summary>
    /// Custom RTMP settings (fully customizable RTMP credentials)
    /// </summary>
    public struct CustomRTMPStreamingService
    {
        /// <summary>
        /// RTMP server URL
        /// </summary>
        public string ServerAddress;
        /// <summary>
        /// RTMP stream key (URL suffix)
        /// </summary>
        public string StreamKey;
        /// <summary>
        /// Tell OBS RTMP client to authenticate to the server
        /// </summary>
        public bool UseAuthentication;
        /// <summary>
        /// Username used if authentication is enabled
        /// </summary>
        public string AuthUsername;
        /// <summary>
        /// Password used if authentication is enabled
        /// </summary>
        public string AuthPassword;
        /// <summary>
        /// Construct object from data provided by <see cref="StreamingService.Settings"/>
        /// </summary>
        /// <param name="settings"></param>
        public CustomRTMPStreamingService(JObject settings)
        {
            ServerAddress = (string)settings["server"];
            StreamKey = (string)settings["key"];
            UseAuthentication = (bool)settings["use_auth"];
            AuthUsername = (string)settings["username"];
            AuthPassword = (string)settings["password"];
        }
        /// <summary>
        /// Convert to JSON object
        /// </summary>
        /// <returns></returns>
        public JObject ToJSON()
        {
            var obj = new JObject();
            obj.Add("server", ServerAddress);
            obj.Add("key", StreamKey);
            obj.Add("use_auth", UseAuthentication);
            obj.Add("username", AuthUsername);
            obj.Add("password", AuthPassword);
            return obj;
        }
    }
    /// <summary>
    /// Crop coordinates for a scene item
    /// </summary>
    public struct SceneItemCropInfo
    {
        /// <summary>
        /// Top crop (in pixels)
        /// </summary>
        public int Top;
        /// <summary>
        /// Bottom crop (in pixels)
        /// </summary>
        public int Bottom;
        /// <summary>
        /// Left crop (in pixels)
        /// </summary>
        public int Left;
        /// <summary>
        /// Right crop (in pixels)
        /// </summary>
        public int Right;
    }
    /// <summary>
    /// BrowserSource source properties
    /// </summary>
    public struct BrowserSourceProperties
    {
        /// <summary>
        /// URL to load in the embedded browser
        /// </summary>
        public string URL;
        /// <summary>
        /// true if the URL points to a local file, false otherwise
        /// </summary>
        public bool IsLocalFile;
        /// <summary>
        /// Additional CSS to apply to the page
        /// </summary>
        public string CustomCSS;
        /// <summary>
        /// Embedded browser render (viewport) width
        /// </summary>
        public int Width;
        /// <summary>
        /// Embedded browser render (viewport) height
        /// </summary>
        public int Height;
        /// <summary>
        /// Embedded browser render frames per second
        /// </summary>
        public int FPS;
        /// <summary>
        /// true if source should be disabled (inactive) when not visible, false otherwise
        /// </summary>
        public bool ShutdownWhenNotVisible;
        /// <summary>
        /// true if source should be visible, false otherwise
        /// </summary>
        public bool Visible;
        /// <summary>
        /// Construct the object from JSON response data
        /// </summary>
        /// <param name="props"></param>
        public BrowserSourceProperties(JObject props)
        {
            URL = (string)props["url"];
            IsLocalFile = (bool)props["is_local_file"];
            CustomCSS = (string)props["css"];
            Width = (int)props["width"];
            Height = (int)props["height"];
            FPS = (int)props["fps"];
            ShutdownWhenNotVisible = (bool)props["shutdown"];
            Visible = (bool)props["render"];
        }
        /// <summary>
        /// Convert the object back to JSON
        /// </summary>
        /// <returns></returns>
        public JObject ToJSON()
        {
            var obj = new JObject();
            obj.Add("url", URL);
            obj.Add("is_local_file", IsLocalFile);
            obj.Add("css", CustomCSS);
            obj.Add("width", Width);
            obj.Add("height", Height);
            obj.Add("fps", FPS);
            obj.Add("shutdown", ShutdownWhenNotVisible);
            obj.Add("render", Visible);
            return obj;
        }
    }
    /// <summary>
    /// Thrown if authentication fails
    /// </summary>
    public class AuthFailureException : Exception
    {

    }
    /// <summary>
    /// Thrown when the server responds with an error
    /// </summary>
    public class ErrorResponseException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message"></param>
        public ErrorResponseException(string message) : base(message)
        {

        }
    }
}
