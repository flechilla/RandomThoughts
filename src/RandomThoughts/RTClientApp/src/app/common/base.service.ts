import { Injectable } from '@angular/core';

import { HttpClient } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class BaseService {

  constructor(private http: HttpClient) { }

  getAntiforgeryToken(): Observable<HttpHeaders> {
    return this.http.get<IAntiForgeryTokenResponse>(urls.GetXsrfTokenUrl)
               .pipe(map(tokenResponse => new HttpHeaders({ 'XSRF-TOKEN' : tokenResponse.antiForgeryToken })));
  }
}

interface IAntiForgeryTokenResponse {
  antiForgeryToken: string;
}
