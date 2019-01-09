﻿// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using Microsoft.Azure.Commands.Insights.OutputClasses;
using Microsoft.Azure.Management.Monitor.Models;
using System.Collections.Generic;
using System.Management.Automation;

namespace Microsoft.Azure.Commands.Insights.ScheduledQueryRules
{
    /// <summary>
    /// Create a ScheduledQueryRule Metric Trigger object
    /// </summary>
    [Cmdlet(VerbsCommon.New, ResourceManager.Common.AzureRMConstants.AzureRMPrefix + "ScheduledQueryRuleMetricTrigger"), OutputType(typeof(PSScheduledQueryRuleMetricTrigger))]
    public class NewScheduledQueryRuleMetricTriggerCommand : MonitorCmdletBase
    {

        #region Cmdlet parameters

        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, HelpMessage = "The metric threshold operator : GreaterThan, LessThan, Equal")]
        [ValidateNotNullOrEmpty]
        public string ThresholdOperator { get; set; }

        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, HelpMessage = "The metric threshold value")]
        public double Threshold { get; set; }

        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, HelpMessage = "The metric trigger type")]
        [ValidateNotNullOrEmpty]
        public string MetricTriggerType { get; set; }

        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, HelpMessage = "Column on which metric value is being aggregated")]
        public string MetricColumn { get; set; }

        #endregion
        protected override void ProcessRecordInternal()
        {
            LogMetricTrigger metricTrigger = new LogMetricTrigger(ThresholdOperator, Threshold, MetricTriggerType, MetricColumn);
            WriteObject(new PSScheduledQueryRuleMetricTrigger(metricTrigger));
        }
    }
}
