import { Injectable } from '@angular/core';
import { Book } from '../models/book.model';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class BooksService {
  baseApiUrl: string = "http://localhost:5103";

  constructor(private http: HttpClient) { }

  getAllBooks(input: string, currentPage: number, itemsPerPage: number): Observable<Book[]> {
    return this.http.get<Book[]>(this.baseApiUrl + '/Books?input=' + input + '&currentPage=' + currentPage + '&itemsPerPage=' + itemsPerPage);
  }


  getBook(id: string): Observable<Book> {
    return this.http.get<Book>(this.baseApiUrl + '/api/books/' + id);
  }

}
