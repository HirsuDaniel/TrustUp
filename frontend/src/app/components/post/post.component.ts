import { Component, OnInit } from '@angular/core';
import { Post } from 'app/models/posts/post.model';
import { PostService } from 'app/services/posts/post.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrl: './post.component.scss'
})
export class PostComponent implements OnInit {

  posts: Post[] = [];
  postForm: FormGroup;
  
  constructor(private formBuilder: FormBuilder, private postService: PostService) {
    this.postForm = this.formBuilder.group({
      content: ['', Validators.required],
      postImage: [null, Validators.required]
    });
  }

  ngOnInit(): void {
    this.fetchPosts();
  }

  fetchPosts(): void {
    this.postService.getPosts().subscribe(
      (posts: Post[]) => {
        this.posts = posts;
      },
      (error) => {
        console.error('Error fetching posts: ', error);
      }
    );
  }

  onFileChange(event: any): void {
    if (event.target.files && event.target.files.length) {
      const file = event.target.files[0];
      this.postForm.patchValue({
        postImage: file
      });
      this.postForm.get('postImage')!.updateValueAndValidity();
    }
  }
  
  onSubmit(): void {
    if (this.postForm.valid) {
      const formData = new FormData();
      formData.append('content', this.postForm.value.content);
      formData.append('postImage', this.postForm.value.postImage);
  
      this.postService.createPost(formData).subscribe(
        (response) => {
          console.log('Post created successfully:', response);
          this.fetchPosts(); 
          this.postForm.reset();
        },
        (error) => {
          console.error('Error creating post:', error);
        }
      );
    }
  }
}
