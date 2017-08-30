﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tera.Game.Messages;

namespace DamageMeter.Heuristic
{
    class S_USER_STATUS : AbstractPacketHeuristic
    {
        public static S_USER_STATUS Instance => _instance ?? (_instance = new S_USER_STATUS());
        private static S_USER_STATUS _instance;

        public S_USER_STATUS() : base(OpcodeEnum.S_USER_STATUS) { }

        public new void Process(ParsedMessage message)
        {
            base.Process(message);
            if (IsKnown || OpcodeFinder.Instance.IsKnown(message.OpCode)) return;

            if (message.Payload.Count != 8 + 4 + 1) return;
            var target = Reader.ReadUInt64();
            var status = Reader.ReadUInt32();
            if(status != 0 && status != 1 && status != 2) return;
            var unk = Reader.ReadByte();
            if (unk != 0) return;

            OpcodeFinder.Instance.SetOpcode(message.OpCode, OPCODE);
        }
    }
}