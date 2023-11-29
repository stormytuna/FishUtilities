using System;
using System.Linq;
using HarmonyLib;

namespace FishUtilities
{
    public static class TerminalHelper
    {
        public static TerminalKeyword GetKeyword(this Terminal terminal, string keywordName) => terminal.terminalNodes.allKeywords.First(kw => kw.name == keywordName);

        public static bool TryGetKeyword(this Terminal terminal, string keywordName, out TerminalKeyword keyword) {
            try {
                keyword = terminal.GetKeyword(keywordName);
                return true;
            } catch {
                keyword = null;
                return false;
            }
        }

        public static TerminalNode GetNodeAfterConfirmation(this TerminalNode node) => node.terminalOptions.First(cn => cn.noun.name.Equals("confirm", StringComparison.InvariantCultureIgnoreCase)).result;

        public static bool TryGetNodeAfterConfirmation(this TerminalNode node, out TerminalNode confirmNode) {
            try {
                confirmNode = node.GetNodeAfterConfirmation();
                return true;
            } catch {
                confirmNode = null;
                return false;
            }
        }

        public static void AddKeyword(this Terminal terminal, TerminalKeyword newKeyword) {
            terminal.terminalNodes.allKeywords = terminal.terminalNodes.allKeywords.AddToArray(newKeyword);
        }

        public static void AddKeywords(this Terminal terminal, params TerminalKeyword[] newKeywords) {
            terminal.terminalNodes.allKeywords = terminal.terminalNodes.allKeywords.AddRangeToArray(newKeywords);
        }

        public static void AddCompatibleNounToKeyword(this Terminal terminal, string keywordName, CompatibleNoun newCompatibleNoun) {
            TerminalKeyword keyword = terminal.terminalNodes.allKeywords.FirstOrDefault(kw => kw.name == keywordName) ?? throw new ArgumentException($"Failed to find keyword with name {keywordName}");
            keyword.compatibleNouns = keyword.compatibleNouns.AddToArray(newCompatibleNoun);
        }

        public static void AddCompatibleNounToKeyword(this Terminal terminal, Func<TerminalKeyword, bool> predicate, CompatibleNoun newCompatibleNoun) {
            TerminalKeyword keyword = terminal.terminalNodes.allKeywords.FirstOrDefault(predicate) ?? throw new ArgumentException($"Failed to find keyword that matches provided predicate");
            keyword.compatibleNouns = keyword.compatibleNouns.AddToArray(newCompatibleNoun);
        }

        public static void AddCompatibleNounsToKeyword(this Terminal terminal, string keywordName, params CompatibleNoun[] newCompatibleNouns) {
            TerminalKeyword keyword = terminal.terminalNodes.allKeywords.FirstOrDefault(kw => kw.name == keywordName) ?? throw new ArgumentException($"Failed to find keyword with name {keywordName}");
            keyword.compatibleNouns = keyword.compatibleNouns.AddRangeToArray(newCompatibleNouns);
        }

        public static void AddCompatibleNounsToKeyword(this Terminal terminal, Func<TerminalKeyword, bool> predicate, params CompatibleNoun[] newCompatibleNouns) {
            TerminalKeyword keyword = terminal.terminalNodes.allKeywords.FirstOrDefault(predicate) ?? throw new ArgumentException($"Failed to find keyword that matches provided predicate");
            keyword.compatibleNouns = keyword.compatibleNouns.AddRangeToArray(newCompatibleNouns);
        }
    }
}
