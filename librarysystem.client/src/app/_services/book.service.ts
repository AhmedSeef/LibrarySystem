import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Book } from '../_models/book';
import { BookAuthorsAndPublisher } from '../_models/BookAuthorsAndPublisher';
import { PublishersAuthors } from '../_models/PublishersAuthors';

@Injectable({
  providedIn: 'root'
})
export class BookService {
  private apiUrl = 'https://localhost:7117/api/v1/Book';

  constructor(private http: HttpClient) { }

  getBooks(includeDeleted: boolean = false, version: string): Observable<BookAuthorsAndPublisher[]> {
    const params = new HttpParams().set('includeDeleted', includeDeleted.toString()).set('version', version);

    return this.http.get<BookAuthorsAndPublisher[]>(`${this.apiUrl}`, { params });
  }

  getBook(id: number, version: string): Observable<BookAuthorsAndPublisher> {
    const params = new HttpParams().set('version', version);

    return this.http.get<BookAuthorsAndPublisher>(`${this.apiUrl}/${id}`, { params });
  }

  addBook(book: Book, version: string): Observable<Book> {
    return this.http.post<Book>(`${this.apiUrl}?version=${version}`, book);
  }

  updateBook(id: number, book: Book, version: string): Observable<Book> {
    return this.http.put<Book>(`${this.apiUrl}/${id}?version=${version}`, book);
  }

  deleteBook(id: number, version: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}?version=${version}`);
  }

  getAuthorsAndPublishers(version: string): Observable<PublishersAuthors> {
    const params = new HttpParams().set('version', version);

    return this.http.get<PublishersAuthors>(`${this.apiUrl}/GetPublishersAndAuthorsLookup`, { params });
  }
}
