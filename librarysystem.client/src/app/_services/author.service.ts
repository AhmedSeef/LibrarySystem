import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Author } from '../_models/author';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthorService {
  private apiUrl = 'https://localhost:7117/api/v1/Author';

  constructor(private http: HttpClient) { }

  getAuthors(includeDeleted: boolean = false, version: string): Observable<Author[]> {
    const params = new HttpParams().set('includeDeleted', includeDeleted.toString()).set('version', version);

    return this.http.get<Author[]>(`${this.apiUrl}`, { params });
  }

  getAuthor(id: number, version: string): Observable<Author> {
    const params = new HttpParams().set('version', version);

    return this.http.get<Author>(`${this.apiUrl}/${id}`, { params });
  }

  addAuthor(author: Author, version: string): Observable<Author> {
    return this.http.post<Author>(`${this.apiUrl}?version=${version}`, author);
  }

  updateAuthor(id: number, author: Author, version: string): Observable<Author> {
    return this.http.put<Author>(`${this.apiUrl}/${id}?version=${version}`, author);
  }

  deleteAuthor(id: number, version: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}?version=${version}`);
  }
}
