using System.ComponentModel.DataAnnotations;

namespace WMS.Infrastructure.Configurations;

public class EmailConfiguration
{
    public const string SectionName = "EmailConfiguration";

    [Required]
	[EmailAddress]
    public required string From { get; set; }

	[Required]
	public required string Server { get; set; }
	
    [Required]
	public required int Port { get; set; }

	[Required]
	public required string UserName { get; set; }

	[Required]
	public required string Password { get; set; }
}