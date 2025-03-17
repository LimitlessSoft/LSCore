namespace Sample.SortAndPage.Api.Entities;

public class ProductEntity
{
	public long Id { get; set; }
	public string Name { get; set; }
	public DateTime CreatedAt { get; set; }
	public bool IsActive { get; set; }
}
