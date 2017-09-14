﻿using System;
using JetBrains.Application.Settings;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon.JavaScript.Util;
using JetBrains.ReSharper.Daemon.Stages;
using JetBrains.ReSharper.Daemon.UsageChecking;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Plugins.Unity.Cg.Psi.Tree;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace JetBrains.ReSharper.Plugins.Unity.Cg.Daemon.Stages
{
    [DaemonStage(StagesBefore = new[] { typeof(GlobalFileStructureCollectorStage) },
        StagesAfter = new [] { typeof(CollectUsagesStage)} )]
    public class IdentifierHighlightingStage : CgDaemonStageBase
    {
        protected override IDaemonStageProcess CreateProcess(IDaemonProcess process, IContextBoundSettingsStore settings,
            DaemonProcessKind processKind, ICgFile file)
        {
            return new IdentifierHighlightingProcess(process, file, settings);
        }

        private class IdentifierHighlightingProcess : CgDaemonStageProcessBase
        {
            private readonly CgIdentifierHighlighter myIdentifierHighlighter;

            public IdentifierHighlightingProcess(IDaemonProcess daemonProcess, ICgFile file, IContextBoundSettingsStore settingsStore)
                : base(daemonProcess, file, settingsStore)
            {
                myIdentifierHighlighter = new CgIdentifierHighlighter();
            }

            public override void VisitNode(ITreeNode node, IHighlightingConsumer context)
            {
                if (node is ITokenNode tokenNode && tokenNode.GetTokenType().IsWhitespace)
                    return;
                
                myIdentifierHighlighter.Highlight(node, context);
            }
        }
    }
}