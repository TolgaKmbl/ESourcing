﻿using ESourcing.Sourcing.Entities;

namespace ESourcing.Sourcing.Repositories.Contract
{
    public interface IBidRepository
    {
        Task SendBid(Bid bid);
        Task<List<Bid>> GetBidsByAuctionId(string id);
        Task<Bid> GetWinnerBid(string id);
    }
}
