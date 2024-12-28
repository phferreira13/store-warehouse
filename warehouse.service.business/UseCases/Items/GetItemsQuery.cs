namespace warehouse.service.business.UseCases.Items;
public class GetItemsQuery : IRequest<IEnumerable<Item>>
{
    internal class Handler(IItemRepository itemRepository) : IRequestHandler<GetItemsQuery, IEnumerable<Item>>
    {
        public async Task<IEnumerable<Item>> Handle(GetItemsQuery request, CancellationToken cancellationToken)
        {
            return await itemRepository.GetItemsAsync();
        }
    }
}
