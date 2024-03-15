import { RestService, Rest } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { IActionResult } from '../../../../../microsoft/asp-net-core/mvc/models';

@Injectable({
  providedIn: 'root',
})
export class CmsKitPublicGlobalResourcesService {
  apiName = 'Default';
  

  getGlobalScript = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, IActionResult>({
      method: 'GET',
      url: '/cms-kit/global-resources/script',
    },
    { apiName: this.apiName,...config });
  

  getGlobalStyle = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, IActionResult>({
      method: 'GET',
      url: '/cms-kit/global-resources/style',
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
