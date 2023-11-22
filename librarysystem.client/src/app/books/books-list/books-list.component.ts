import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { BookAuthorsAndPublisher } from 'src/app/_models/BookAuthorsAndPublisher';
import { Book } from 'src/app/_models/book';
import { BookService } from 'src/app/_services/book.service';

@Component({
  selector: 'app-books-list',
  templateUrl: './books-list.component.html',
  styleUrls: ['./books-list.component.css']
})
export class BooksListComponent implements OnInit {
  books: BookAuthorsAndPublisher[] = [];
  includeDeleted: boolean = false;

  constructor(
    private bookService: BookService,
    private router: Router,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.loadBooks();
  }

  openEditModal(book: Book) {
    const bookId = book.id;
    this.router.navigate(['/books', bookId, 'edit']);
  }

  toggleIncludeDeleted() {
    this.includeDeleted = !this.includeDeleted;
    this.loadBooks();
  }

  loadBooks() {
    this.bookService.getBooks(this.includeDeleted, '1').subscribe(books => {
      this.books = books;
    });
  }

  openDeleteModal(book: Book) {
    this.bookService.deleteBook(book.id, '1').subscribe(
      () => {
        this.toastr.success('Book deleted successfully', 'Success', {
          timeOut: 5000,
          closeButton: true,
          positionClass: 'toast-top-right',
        });

        this.loadBooks();
      },
      (error) => {
        this.toastr.error(`Error deleting book: ${error.message}`, 'Error', {
          timeOut: 5000,
          closeButton: true,
          positionClass: 'toast-top-right',
        });

        console.error('Error deleting book:', error);
      }
    );
  }

  openViewModal(book: Book) {
    this.router.navigate(['/books', book.id]);
  }

  redirectToBookForm() {
    this.router.navigate(['/books/add']);
  }
}