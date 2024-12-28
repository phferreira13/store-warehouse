namespace warehouse.service.business.UseCases.Items;
public class CreateItemCommand : IRequest<Item>
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }

    internal class Handler(IItemRepository itemRepository) : IRequestHandler<CreateItemCommand, Item>
    {
        public async Task<Item> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            var item = new Item(request.Name, request.Price, request.Description);

            await itemRepository.AddItemAsync(item);

            return item;
        }
    }
}
