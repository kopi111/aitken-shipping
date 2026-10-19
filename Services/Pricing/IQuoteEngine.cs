using AitkenShipping.Models;

namespace AitkenShipping.Services.Pricing;

public interface IQuoteEngine
{
    QuoteResult Quote(QuoteRequest request);
}
