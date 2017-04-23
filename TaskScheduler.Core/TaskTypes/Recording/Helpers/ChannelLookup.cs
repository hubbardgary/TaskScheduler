using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using TaskScheduler.Core.Models.Recording;

namespace TaskScheduler.Core.Helpers
{
    public static class ChannelLookup
    {
        static ChannelLookup()
        {
            var channels = new Dictionary<int, string>();

            foreach (var channel in ConfigurationManager.AppSettings["channels"].Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                string[] values = channel.Split(',');
                if (values.Length == 2)
                    channels.Add(Convert.ToInt32(values[0]), values[1]);
            }

            AllChannels = channels;
        }

        public static readonly Dictionary<int, string> AllChannels;

        public static ChannelModel GetChannel(int channelId)
        {
            if (!AllChannels.ContainsKey(channelId))
            {
                throw new Exception($"Channel id does not exist: {channelId}");
            }

            return new ChannelModel(channelId, AllChannels[channelId]);
        }

        public static ChannelModel GetChannel(string channelName)
        {
            if (!AllChannels.ContainsValue(channelName))
            {
                throw new Exception($"Channel name does not exist: {channelName}");
            }
            
            return new ChannelModel(AllChannels.Where(c => c.Value == channelName).First().Key, channelName);
        }
    }
}
