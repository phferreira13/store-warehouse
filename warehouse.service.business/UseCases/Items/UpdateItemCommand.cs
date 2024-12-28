namespace warehouse.service.business.UseCases.Items;
public class UpdateItemCommand : IRequest
{
    private int Id { get; set; }
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public required string Description { get; set; }
    public UpdateItemCommand SetId(int id)
    {
        Id = id;
        return this;
    }

    internal class Handler(IItemRepository itemRepository) : IRequestHandler<UpdateItemCommand>
    {
        public async Task Handle(UpdateItemCommand request, CancellationToken cancellationToken)
        {
            var item = await itemRepository.GetItemAsync(request.Id)
                ?? throw new Exception("Item not found");
            await itemRepository.UpdateItemAsync(request.Id, request.Name, request.Price, request.Description);
        }
    }
}
