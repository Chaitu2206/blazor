using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zindagi.Infra.Options
{
    public class ApplicationOptions
    {
        public const string AppSettingsSection = "Application";

        [Required]
        public string Id { get; set; } = null!;

        [Required]
        [RegularExpression(@"^[a-z0-9_]*$")]
        public string Slug { get; set; } = null!;

        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,50}$")]
        public string Name { get; set; } = null!;

        [DisplayName("Base URL")]
        [Required]
        [Url(ErrorMessage = "Invalid Base URL")]
        public string BaseUrl { get; set; } = null!;

        public string Version { get; set; } = null!;

        [Required]
        public string DataDirectory { get; set; } = null!;

        public Hashtable? Custom { get; set; }
    }
}
