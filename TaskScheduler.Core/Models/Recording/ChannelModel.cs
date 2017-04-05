using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskScheduler.Core.Models.Recording
{
    public class ChannelModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Dictionary<int, string> ChannelList => new Dictionary<int, string>
        {
            { 0, "zero" },
            { 1, "BBC One"}
        };

        public ChannelModel() { }

        public ChannelModel(int id)
        {
            if (!ChannelList.ContainsKey(id))
            {
                throw new Exception($"Channel id does not exist: {id}");
            }

            Id = id;
            Name = ChannelList[id];
        }

        public ChannelModel(string name)
        {
            if (!ChannelList.ContainsValue(name))
            {
                throw new Exception($"Channel name does not exist: {name}");
            }

            var channel = ChannelList.Where(c => c.Value == name).FirstOrDefault();

            Id = channel.Key;
            Name = channel.Value;
        }
    }
}
