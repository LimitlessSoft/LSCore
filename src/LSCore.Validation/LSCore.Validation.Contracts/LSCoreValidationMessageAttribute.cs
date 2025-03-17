using System.ComponentModel;

namespace LSCore.Validation.Contracts;

public class LSCoreValidationMessageAttribute(string description)
	: DescriptionAttribute(description);
