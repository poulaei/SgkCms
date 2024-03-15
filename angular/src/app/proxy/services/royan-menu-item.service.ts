import { RestService, Rest } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { HierarchyNode } from '../models';
import type { RoyanMenuItemDto } from '../volo/cms-kit/menus/models';

@Injectable({
  providedIn: 'root',
})
export class RoyanMenuItemService {
  apiName = 'Default';
  

  getList = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, HierarchyNode<RoyanMenuItemDto>>({
      method: 'GET',
      url: '/api/app/royan-menu-item',
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
