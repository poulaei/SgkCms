import type { BoxDto } from './boxes/models';
import { RestService, Rest } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class BoxService {
  apiName = 'Default';
  

  getBoxItems = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, BoxDto>({
      method: 'GET',
      url: `/api/app/box/${id}/box-items`,
    },
    { apiName: this.apiName,...config });
  

  getDetailedList = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, BoxDto[]>({
      method: 'GET',
      url: '/api/app/box/detailed-list',
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
