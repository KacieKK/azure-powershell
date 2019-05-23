//
// Copyright (c) Microsoft and contributors.  All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//
// See the License for the specific language governing permissions and
// limitations under the License.
//

// Warning: This code was generated by a tool.
//
// Changes to this file may cause incorrect behavior and will be lost if the
// code is regenerated.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Azure.Commands.Compute.Automation.Models;
using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;
using Microsoft.Azure.Management.Compute;
using Microsoft.Azure.Management.Compute.Models;
using Microsoft.WindowsAzure.Commands.Utilities.Common;

namespace Microsoft.Azure.Commands.Compute.Automation
{
    [Cmdlet(VerbsCommon.Get, ResourceManager.Common.AzureRMConstants.AzureRMPrefix + "Image", DefaultParameterSetName = "DefaultParameter")]
    [OutputType(typeof(PSImage))]
    public partial class GetAzureRmImage : ComputeAutomationBaseCmdlet
    {
        public override void ExecuteCmdlet()
        {
            base.ExecuteCmdlet();
            ExecuteClientAction(() =>
            {
                string resourceGroupName = this.ResourceGroupName;
                string imageName = this.ImageName;
                string expand = this.Expand;

                if (ShouldGetByName(resourceGroupName, imageName))
                {
                    var result = ImagesClient.Get(resourceGroupName, imageName, expand);
                    var psObject = new PSImage();
                    ComputeAutomationAutoMapperProfile.Mapper.Map<Image, PSImage>(result, psObject);
                    WriteObject(psObject);
                }
                else if (ShouldListByResourceGroup(resourceGroupName, imageName))
                {
                    var result = ImagesClient.ListByResourceGroup(resourceGroupName);
                    var resultList = result.ToList();
                    var nextPageLink = result.NextPageLink;
                    while (!string.IsNullOrEmpty(nextPageLink))
                    {
                        var pageResult = ImagesClient.ListByResourceGroupNext(nextPageLink);
                        foreach (var pageItem in pageResult)
                        {
                            resultList.Add(pageItem);
                        }
                        nextPageLink = pageResult.NextPageLink;
                    }
                    var psObject = new List<PSImageList>();
                    foreach (var r in resultList)
                    {
                        psObject.Add(ComputeAutomationAutoMapperProfile.Mapper.Map<Image, PSImageList>(r));
                    }
                    WriteObject(TopLevelWildcardFilter(resourceGroupName, imageName, psObject), true);
                }
                else
                {
                    var result = ImagesClient.List();
                    var resultList = result.ToList();
                    var nextPageLink = result.NextPageLink;
                    while (!string.IsNullOrEmpty(nextPageLink))
                    {
                        var pageResult = ImagesClient.ListNext(nextPageLink);
                        foreach (var pageItem in pageResult)
                        {
                            resultList.Add(pageItem);
                        }
                        nextPageLink = pageResult.NextPageLink;
                    }
                    var psObject = new List<PSImageList>();
                    foreach (var r in resultList)
                    {
                        psObject.Add(ComputeAutomationAutoMapperProfile.Mapper.Map<Image, PSImageList>(r));
                    }
                    WriteObject(TopLevelWildcardFilter(resourceGroupName, imageName, psObject), true);
                }
            });
        }

        [Parameter(
            ParameterSetName = "DefaultParameter",
            Position = 0,
            ValueFromPipelineByPropertyName = true)]
        [ResourceGroupCompleter]
        [SupportsWildcards]
        public string ResourceGroupName { get; set; }

        [Parameter(
            ParameterSetName = "DefaultParameter",
            Position = 1,
            ValueFromPipelineByPropertyName = true)]
        [ResourceNameCompleter("Microsoft.Compute/images", "ResourceGroupName")]
        [SupportsWildcards]
        [Alias("Name")]
        public string ImageName { get; set; }

        [Parameter(
            ParameterSetName = "DefaultParameter",
            Position = 2,
            ValueFromPipelineByPropertyName = true)]
        public string Expand { get; set; }
    }
}
