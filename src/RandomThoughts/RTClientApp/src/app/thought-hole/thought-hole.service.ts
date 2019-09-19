import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

/**
 * This hole contains the functions for the communication
 * with the server to handle the ThoughtHole data.
 * 
 * Also it's used for the business logic of this data.
 *
 * @export
 * @class ThoughtHoleService
 */
@Injectable({
  providedIn: 'root'
})
export class ThoughtHoleService {

  constructor(httpClient: HttpClient) { }
}
