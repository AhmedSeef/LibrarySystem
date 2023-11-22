import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Author } from 'src/app/_models/author';
import { AuthorService } from 'src/app/_services/author.service';
import { ToastrService } from 'ngx-toastr';
import { trigger, transition, animate, style } from '@angular/animations';

@Component({
  selector: 'app-author-add',
  templateUrl: './author-add.component.html',
  styleUrls: ['./author-add.component.css'],
  animations: [
    trigger('flyInOut', [
      transition('void => *', [
        style({ opacity: 0, transform: 'translateX(-100%)' }),
        animate('500ms ease-in', style({ opacity: 1, transform: 'translateX(0)' })),
      ]),
      transition('* => void', [
        animate('500ms ease-out', style({ opacity: 0, transform: 'translateX(100%)' })),
      ]),
    ]),
  ],
})
export class AuthorAddComponent {
  isNameValid: boolean = false;

  author: Author = {
    id: 0,
    name: '',
    createdAt: new Date(),
    isDeleted: false,
  };

  constructor(private authorService: AuthorService,
     private router: Router, private toastr: ToastrService) {}

  onSubmit() {
    this.authorService.addAuthor(this.author, '1').subscribe(
      (newAuthor) => {
        this.toastr.success('Author added successfully', 'Success', {
          timeOut: 5000,
          closeButton: true,
          positionClass: 'toast-top-right',
        });

        this.router.navigate(['/authors']);
      },
      (error) => {
        const errorMessage = error?.error?.message || 'An error occurred while adding the author.';

        this.toastr.error(`Error adding author: ${errorMessage}`, 'Error', {
          timeOut: 5000,
          closeButton: true,
          positionClass: 'toast-top-right',
        });
      }
    );
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