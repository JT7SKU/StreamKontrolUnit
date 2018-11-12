using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace AoP.Patreon.Resources
{
    public class Media
    {
        [JsonProperty("file_name")]
        public string Filename { get; set; }
        [JsonProperty("size_bytes")]
        public int SizeBytes { get; set; }
        [JsonProperty("mimetype")]
        public string MimeType { get; set; }
        [JsonProperty("state")]
        public string State { get; set; }
        [JsonProperty("owner_type")]
        public string OwnerType { get; set; }
        [JsonProperty("owner_id")]
        public string OwnerId { get; set; }
        [JsonProperty("owner_relationship")]
        public string OwnerRelationship { get; set; }
        [JsonProperty("upload_expires_at")]
        public DateTime UploadExpiresAt { get; set; }
        [JsonProperty("upload_url")]
        public string UploadUrl { get; set; }
        [JsonProperty("upload_parameters")]
        public string UploadParameters { get; set; }
        [JsonProperty("download_url")]
        public string DownloadUrl { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("metadata")]
        public string Metadata { get; set; }
    }
}
