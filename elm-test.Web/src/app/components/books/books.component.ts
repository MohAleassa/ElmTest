import { Component, HostListener, Input, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { Book } from 'src/app/models/book.model';
import { BooksService } from 'src/app/services/books.service';

@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.css'],
})
export class BooksComponent implements OnInit {
  books: Book[] = [];
  input: string = '';
  invalidSearchFeedback: string = '';
  isLoading: boolean = false;
  currentPage = 1;
  itemsPerPage = 10;

  constructor(
    private bookService: BooksService,
    private router: Router,
    private sanitizer: DomSanitizer
  ) {
  }

  ngOnInit(): void {

  }

  modelChangeFn(value: any) {
    if (value === '' || !value.trim()) {
      this.invalidSearchFeedback = 'This field can not be blank.';
      return;
    } else if (!/^[\p{Alphabetic}\p{Mark}\p{Decimal_Number}\p{Connector_Punctuation}\p{Join_Control}]+$/u.test(value)) {
      this.invalidSearchFeedback = 'This field can not contain special characters.';
      return;
    }

    this.invalidSearchFeedback = '';
    this.currentPage = 1;
    this.fillData(value);
  }
  @HostListener('window:scroll', ['$event'])
  onWindowScroll(event: any) {
    if (window.innerHeight + window.scrollY >= document.body.offsetHeight && !this.isLoading) {
      this.currentPage++;
      this.appendData();
    }
  }

  async fillData(input: string) {
    this.isLoading = true;
    await this.bookService.getAllBooks(input, this.currentPage, this.itemsPerPage).subscribe({
      next: (books) => {
        books.forEach( (book) => {
          book.coverSrc = this.sanitizer.bypassSecurityTrustResourceUrl(book.coverBase64);
        });
        
        this.books = books;
      },
      error: (response) => {
        console.log(response);
      },
      complete: () => {
        this.isLoading = false;
      }
    });
  }

  appendData() {
    this.isLoading = true;
    this.bookService.getAllBooks(this.input, this.currentPage, this.itemsPerPage).subscribe({
      next: (books) => {
        books.forEach( (book) => {
          book.coverSrc = this.sanitizer.bypassSecurityTrustResourceUrl(book.coverBase64);
        });
        this.books = [...this.books, ...books];
      },
      error: (response) => {
        console.log(response);
      },
      complete: () => {
        this.isLoading = false;
      }
    });
  }

}
