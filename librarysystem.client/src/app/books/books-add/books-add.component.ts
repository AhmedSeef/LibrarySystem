import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { PublishersAuthors } from 'src/app/_models/PublishersAuthors';
import { Author } from 'src/app/_models/author';
import { Book } from 'src/app/_models/book';
import { Publisher } from 'src/app/_models/publisher';
import { BookService } from 'src/app/_services/book.service';

@Component({
  selector: 'app-books-add',
  templateUrl: './books-add.component.html',
  styleUrls: ['./books-add.component.css']
})
export class BooksAddComponent implements OnInit {
  // Data for dropdowns
  isNameValid: boolean = false;
  authors: Author[] = [];
  publishers: Publisher[] = [];

  // Book model
  book: Book = {
    id: 0,
    name: '',
    authorId: 0,
    publisherId: 0,
    createdAt: new Date(),
    isDeleted: false,
  };

  constructor(private bookService: BookService
    , private toastr: ToastrService
    ,private router: Router) { }

  ngOnInit(): void {
    this.loadAuthorsAndPublishers('1');
  }

  loadAuthorsAndPublishers(version: string): void {
    this.bookService.getAuthorsAndPublishers(version).subscribe(
      (data: PublishersAuthors) => {
        this.authors = data.authors;
        this.publishers = data.publishers;
      },
      (error) => {
      }
    );
  }

  onSubmit(): void {
    console.log(this.book)
    this.bookService.addBook(this.book, '1').subscribe(
      (addedBook) => {
        this.showToast('success', 'Book added successfully');
        this.router.navigate(['/authors']);
      },
      (error) => {
        const errorMessage = error?.error?.message || 'An error occurred while adding the book.';
        this.showToast('error', `Error adding book: ${errorMessage}`);
      }
    );
  }
  
  onNameChange() {
    this.isNameValid = this.validateName();
  }

  validateName(): boolean {
    const name = this.book.name;
    const isValid = name.length >= 10 && name.length <= 50;

    if (!isValid) {
      this.toastr.warning('Name must be between 10 and 50 characters', 'Warning', {
        timeOut: 5000,
        closeButton: true,
        positionClass: 'toast-top-right',
      });
    }

    return isValid;
  }

  private showToast(type: 'success' | 'error', message: string): void {
    const toastrOptions = {
      timeOut: 5000,
      closeButton: true,
      positionClass: 'toast-top-right',
    };

    if (type === 'success') {
      this.toastr.success(message, 'Success', toastrOptions);
    } else {
      this.toastr.error(message, 'Error', toastrOptions);
    }
  }
}
