using System;
using System.ComponentModel.DataAnnotations;

namespace TtsClient.Models
{
    public class SpeechMetadata
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Key { get; set; } = string.Empty;

        public string Value { get; set; } = string.Empty;

        [Required]
        public Guid SpeechEntryId { get; set; }
    }
}