﻿using AttendanceBot.Models;
using LanguageExt;
using Microsoft.Bot.Connector;
using System.Linq;

namespace AttendanceBot.Commands
{
    public class OutCommand : BotCommandBase
    {
        public override string CommandName { get { return CommandPrefix + "out"; } }

        public override Option<string> Handle(string[] messageElements, Message originalMessage)
        {
            var result = AttendanceRegistry.FindCurrent(originalMessage.ConversationId);
            return result.Match(
                    e =>
                    {
                        e.Out(originalMessage.From.Id, originalMessage.From.Name, 
                            messageElements.Length > 1 ? string.Join(" ", messageElements.Skip(1)) : string.Empty);
                        return $"{originalMessage.From.Name} out";
                    },
                    () => "No active event at the moment"
                );
        }
    }
}