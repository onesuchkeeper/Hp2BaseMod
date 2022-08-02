using Hp2BaseMod.GameDataInfo;
using System;
using System.IO;

namespace Hp2BaseMod
{
    /// <summary>
    /// A log for modded info
    /// </summary>
    public class ModLog
    {
        public class ModLogIndent : IDisposable
        {
            private readonly ModLog _modLog;

            public ModLogIndent(ModLog modLog)
            {
                _modLog = modLog;
                _modLog.IncreaseIndent();
            }

            public void Dispose()
            {
                _modLog.DecreaseIndent();
            }
        }

        /// <summary>
        /// How indented the log messages will be
        /// </summary>
        private int _indent;

        /// <summary>
        /// loader log
        /// </summary>
        private readonly TextWriter _tw;

        public ModLog(string logPath)
        {
            _tw = File.CreateText(logPath);
        }

        public ModLogIndent MakeIndent() => new ModLogIndent(this);

        /// <summary>
        /// Increases how indented the log messages will be
        /// </summary>
        public void IncreaseIndent()// => _indent++;
        {
            //LogLine("{");
            _indent++;
        }
        /// <summary>
        /// Decreases how indented the log messages will be
        /// </summary>
        public void DecreaseIndent()// => _indent = Math.Max(0, _indent - 1);
        {
            _indent = Math.Max(0, _indent - 1);
            //LogLine("}");
        }

        /// <summary>
        /// outputs a formatted error message to the log
        /// </summary>
        /// <param name="message"></param>
        public void LogError(string message)
        {
            var lines = message.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            var tab = new string('-', Math.Max(1, (_indent * 2) - 5));

            foreach (var l in lines)
            {
                _tw.WriteLine($"-ERROR{tab} {l}");
            }

            _tw.Flush();
        }

        /// <summary>
        /// for debugging, logs an error if the target is null.
        /// Returns true if the target is null, false otherwise.
        /// </summary>
        /// <param name="line"></param>
        public bool LogIsNull(object target, string name = null)
        {
            var isNull = target == null;
            if (isNull)
            {
                LogError($"{name} is null");
            }
            return isNull;
        }

        /// <summary>
        /// outputs a newline to the log
        /// </summary>
        /// <param name="line"></param>
        public void LogNewLine() => _tw.WriteLine();

        /// <summary>
        /// outputs to the log
        /// </summary>
        /// <param name="line"></param>

        public void LogLine([System.Runtime.CompilerServices.CallerMemberName] string line = "")
        {
            if (line == null) { line = "null"; }

            var lines = line.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            var tab = new string(' ', _indent * 2);

            foreach (var l in lines)
            {
                _tw.WriteLine(tab + l);
            }

            _tw.Flush();
        }

        /// <summary>
        /// outputs a formatted title to the log
        /// </summary>
        /// <param name="line"></param>
        public void LogTitle(string line)
        {
            LogNewLine();
            LogLine($"-----{line}-----");
        }

        public void LogMissingIdError(string descriptor, RelativeId id) => LogMissingIdError(descriptor, id.LocalId, id.SourceId);

        public void LogMissingIdError(string descriptor, int localId, int SourceId) => LogLine($"{descriptor} with local id {localId} and mod id {SourceId}, but no mod with that id exsists. Make sure you're obtaining your mod ids correctly by looking the mod up from the {nameof(ModInterface)} in {nameof(IHp2ModStarter.Start)}.");
    }
}
