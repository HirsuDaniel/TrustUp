import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Post } from 'app/models/posts/post.model';

@Injectable({
  providedIn: 'root'
})

export class PostService {
  private apiUrl = 'http://localhost:5084/api/posts';

  constructor(private http: HttpClient) { }

  getPosts(): Observable<Post[]>{
    return this.http.get<Post[]>(this.apiUrl);
  }

  createPost(formData: FormData): Observable<any> {
    const headers = new HttpHeaders();
    return this.http.post<any>(this.apiUrl, formData,{ headers });
  }
}
