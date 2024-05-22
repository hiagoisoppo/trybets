using TryBets.Odds.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Globalization;

namespace TryBets.Odds.Repository;

public class OddRepository : IOddRepository
{
    protected readonly ITryBetsContext _context;
    public OddRepository(ITryBetsContext context)
    {
        _context = context;
    }

    public Match Patch(int MatchId, int TeamId, string BetValue)
    {
        Match match = _context.Matches.FirstOrDefault(m => m.MatchId == MatchId);
        if (match is null) return null;

        decimal betAmount = decimal.Parse(BetValue.Replace(",", "."), CultureInfo.InvariantCulture);
        if (match.MatchTeamAId == TeamId)
        {
            match.MatchTeamAValue += betAmount;
        }
        else if (match.MatchTeamBId == TeamId)
        {
            match.MatchTeamBValue += betAmount;
        }
        else
        {
            return null;
        }

        _context.Matches.Update(match);
        _context.SaveChanges();
        return match;
    }
}