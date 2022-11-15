﻿using System;
using System.Linq;
using System.Threading.Tasks;
using LenovoLegionToolkit.Lib.Controllers;

namespace LenovoLegionToolkit.Lib.Automation.Steps
{
    public class SpectrumKeyboardBacklightProfileAutomationStep : IAutomationStep<int>
    {
        private readonly SpectrumKeyboardBacklightController _controller = IoCContainer.Resolve<SpectrumKeyboardBacklightController>();

        private readonly int[] _allStates = Enumerable.Range(1, 6).ToArray();

        public int State { get; }

        public SpectrumKeyboardBacklightProfileAutomationStep(int state) => State = state;

        public Task<int[]> GetAllStatesAsync() => Task.FromResult(_allStates);

        public Task<bool> IsSupportedAsync() => Task.FromResult(_controller.IsSupported());

        public async Task RunAsync()
        {
            if (!_controller.IsSupported())
                return;

            if (!_allStates.Contains(State))
                throw new InvalidOperationException(nameof(State));

            await _controller.SetProfileAsync(State).ConfigureAwait(false);
        }

        public IAutomationStep DeepCopy() => new SpectrumKeyboardBacklightBrightnessAutomationStep(State);
    }
}