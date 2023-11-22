import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Publisher } from '../_models/publisher';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PublisherService {
  private apiUrl = 'https://localhost:7117/api/v1/Publisher';

  constructor(private http: HttpClient) { }

  getPublishers(includeDeleted: boolean = false, version: string): Observable<Publisher[]> {
    const params = new HttpParams().set('includeDeleted', includeDeleted.toString()).set('version', version);

    return this.http.get<Publisher[]>(`${this.apiUrl}`, { params });
  }

  getPublisher(id: number, version: string): Observable<Publisher> {
    const params = new HttpParams().set('version', version);

    return this.http.get<Publisher>(`${this.apiUrl}/${id}`, { params });
  }

  addPublisher(publisher: Publisher, version: string): Observable<Publisher> {
    return this.http.post<Publisher>(`${this.apiUrl}?version=${version}`, publisher);
  }

  updatePublisher(id: number, publisher: Publisher, version: string): Observable<Publisher> {
    return this.http.put<Publisher>(`${this.apiUrl}/${id}?version=${version}`, publisher);
  }

  deletePublisher(id: number, version: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}?version=${version}`);
  }
}
