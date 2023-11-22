import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthorService } from 'src/app/_services/author.service';

@Component({
  selector: 'app-author-details',
  templateUrl: './author-details.component.html',
  styleUrls: ['./author-details.component.css']
})

export class AuthorDetailsComponent implements OnInit {
  author: any = {};

  constructor(private authorService: AuthorService, private route: ActivatedRoute, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.loadAuthorDetails();
  }

  loadAuthorDetails() {
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
        }
      );
    }
  }
}
