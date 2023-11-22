import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Author } from 'src/app/_models/author';
import { AuthorService } from 'src/app/_services/author.service';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-author-edit',
  templateUrl: './author-edit.component.html',
  styleUrls: ['./author-edit.component.css']
})

export class AuthorEditComponent {
  author: Author = {
    id: 0,
    name: '',
    createdAt: new Date(),
    isDeleted: false,
  };
  isNameValid: boolean = true;

  constructor(
    private authorService: AuthorService,
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    const idParam = this.route.snapshot.paramMap.get('id');
    if (idParam !== null) {
      const authorId = +idParam;
      this.authorService.getAuthor(authorId, '1').subscribe(
        (author) => {
          this.author = author;
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

  onSubmit() {
    if (this.isNameValid) {
      this.authorService.updateAuthor(this.author.id, this.author, '1').subscribe(
        (updatedAuthor) => {
          this.toastr.success('Author updated successfully', 'Success', {
            timeOut: 5000,
            closeButton: true,
            positionClass: 'toast-top-right',
          });

          this.router.navigate(['/authors']);
        },
        (error) => {
          this.toastr.error('Error updating author', 'Error', {
            timeOut: 5000,
            closeButton: true,
            positionClass: 'toast-top-right',
          });

          console.error('Error updating author:', error);
        }
      );
    }
  }

  onNameChange() {
    this.isNameValid = this.validateName();
  }

  validateName(): boolean {
    const name = this.author.name;
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
}