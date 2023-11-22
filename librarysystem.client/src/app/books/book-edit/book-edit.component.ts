import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { PublishersAuthors } from 'src/app/_models/PublishersAuthors';
import { Author } from 'src/app/_models/author';
import { Book } from 'src/app/_models/book';
import { Publisher } from 'src/app/_models/publisher';
import { BookService } from 'src/app/_services/book.service';

@Component({
  selector: 'app-book-edit',
  templateUrl: './book-edit.component.html',
  styleUrls: ['./book-edit.component.css']
})
export class BookEditComponent {
  isNameValid: boolean = false;
  authors: Author[] = [];
  publishers: Publisher[] = [];

  // Book model
  book: any;

  constructor(private bookService: BookService
    , private toastr: ToastrService
    , private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {
    this.loadAuthorsAndPublishers('1');
    this.loadBookToEdit();
  }

  loadBookToEdit() {
    const idParam = this.route.snapshot.paramMap.get('id');
    if (idParam !== null) {
      const authorId = +idParam;
      this.bookService.getBook(authorId, '1').subscribe(
        (book) => {
          this.book = book;
          this.book.authorId = this.book.authorDto.id;
          this.book.publisherId = this.book.publisherDto.id;
        },
        (error) => {
          this.toastr.error('Error fetching author details', 'Error', {
            timeOut: 5000,
            closeButton: true,
            positionClass: 'toast-top-right',
          });

          console.error('Error fetching author details:', error);
        }
      );
    }
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
    this.bookService.updateBook(this.book.id,this.book, '1').subscribe(
      (addedBook) => {
        this.showToast('success', 'Book Edited successfully');
        this.router.navigate(['/books']);
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
