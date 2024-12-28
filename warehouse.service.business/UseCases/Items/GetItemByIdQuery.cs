namespace warehouse.service.business.UseCases.Items;
public class GetItemByIdQuery : IRequest<Item?>
{
    public int Id { get; set; }

    internal class Handler(IItemRepository itemRepository) : IRequestHandler<GetItemByIdQuery, Item?>
    {
        public async Task<Item?> Handle(GetItemByIdQuery request, CancellationToken cancellationToken)
        {
            return await itemRepository.GetItemAsync(request.Id);
        }
    }
}
