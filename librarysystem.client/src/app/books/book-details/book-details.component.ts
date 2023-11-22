import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { BookService } from 'src/app/_services/book.service';

@Component({
  selector: 'app-book-details',
  templateUrl: './book-details.component.html',
  styleUrls: ['./book-details.component.css']
})
export class BookDetailsComponent implements OnInit {
  book: any = {};

  constructor(private bookService: BookService,
     private route: ActivatedRoute,
      private toastr: ToastrService) { }

  ngOnInit(): void {
    this.loadBookDetails();
  }

  loadBookDetails() {
    const idParam = this.route.snapshot.paramMap.get('id');

    if (idParam !== null) {
      const bookId = +idParam;

      this.bookService.getBook(bookId, '1').subscribe(
        (book) => {
          this.book = book;
        },
        (error) => {
          this.toastr.error('Error fetching book details', 'Error', {
            timeOut: 5000,
            closeButton: true,
            positionClass: 'toast-top-right',
          });
        }
      );
    }
  }
}
