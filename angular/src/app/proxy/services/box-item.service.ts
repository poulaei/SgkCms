import type { BoxItemDto, CreateBoxItemDto, UpdateBoxItemDto } from './boxes/models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class BoxItemService {
  apiName = 'Default';
  

  create = (input: CreateBoxItemDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, BoxItemDto>({
      method: 'POST',
      url: '/api/app/box-item',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/box-item/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, BoxItemDto>({
      method: 'GET',
      url: `/api/app/box-item/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getDetailedList = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, BoxItemDto[]>({
      method: 'GET',
      url: '/api/app/box-item/detailed-list',
    },
    { apiName: this.apiName,...config });
  

  getList = (input: PagedAndSortedResultRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<BoxItemDto>>({
      method: 'GET',
      url: '/api/app/box-item',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: UpdateBoxItemDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, BoxItemDto>({
      method: 'PUT',
      url: `/api/app/box-item/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
