using TaskScheduler.Core.Helpers;

namespace TaskScheduler.Core.Models.Recording
{
    public class ChannelModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ChannelModel() { }

        public ChannelModel(int id)
        {
            var channel = ChannelLookup.GetChannel(id);
            Id = channel.Id;
            Name = channel.Name;
        }

        public ChannelModel(string name)
        {
            var channel = int.TryParse(name, out int id) ? new ChannelModel(id) : ChannelLookup.GetChannel(name);
            Id = channel.Id;
            Name = channel.Name;
        }

        public ChannelModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
