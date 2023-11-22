import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Author } from 'src/app/_models/author';
import { AuthorService } from 'src/app/_services/author.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-authors-list',
  templateUrl: './authors-list.component.html',
  styleUrls: ['./authors-list.component.css']
})
export class AuthorsListComponent implements OnInit {
  authors: Author[] = [];
  includeDeleted: boolean = false;

  constructor(private authorService: AuthorService,
    private router: Router,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.loadAuthors();
  }

  openEditModal(author: Author) {
    const authorId = author.id;
    this.router.navigate(['/authors', authorId, 'edit']);
  }

  toggleIncludeDeleted() {
    this.includeDeleted = !this.includeDeleted;
    this.loadAuthors();
  }

  loadAuthors() {
    this.authorService.getAuthors(this.includeDeleted, '1').subscribe(authors => {
      this.authors = authors;
    });
  }

  openDeleteModal(author: Author) {
    this.authorService.deleteAuthor(author.id, '1').subscribe(
      () => {
        this.toastr.success('Author deleted successfully', 'Success', {
          timeOut: 5000,
          closeButton: true,
          positionClass: 'toast-top-right',
        });

        this.loadAuthors();
      },
      (error) => {
        this.toastr.error(`Error deleting author: ${error.message}`, 'Error', {
          timeOut: 5000,
          closeButton: true,
          positionClass: 'toast-top-right',
        });

        console.error('Error deleting author:', error);
      }
    );
  }

  openViewModal(author: Author) {
    this.router.navigate(['/authors', author.id]);
  }

  redirectToAuthorForm() {
    this.router.navigate(['/authors/add']);
  }
}
