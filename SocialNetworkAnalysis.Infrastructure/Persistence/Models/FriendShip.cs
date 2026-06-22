using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Infrastructure.Persistence.Models
{
    public class FriendShip
    {
        private int _fromId;
        private int _toId;

        public FriendShip(int fromId, int toId)
        {
            _fromId = Math.Max(fromId, toId);
            _toId = Math.Min(fromId, toId);
        }

        public int FromId { get { return _fromId; } }
        public int ToId { get { return _toId; } }
    }
}
