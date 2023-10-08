import { Component } from '@angular/core';
import { ApiService } from '../services/api.service';
import {DomSanitizer} from '@angular/platform-browser';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls:['./home.component.css']
})
export class HomeComponent {
  textValue: string = 'Enter text';
  name: string = '';
  
  octetStreamData$: Observable<Blob>;
  image: any
  imageToShow: any;
  constructor(private apiService: ApiService, private sanitizer:DomSanitizer) { }

  private octetStreamUrl(data): any | null {
      const blob = new Blob([data], { type: 'image/png' });
      return this.sanitizer.bypassSecurityTrustUrl(URL.createObjectURL(blob) );
  }
  handleSubmit() {
    const request = { Text: this.name };
    this.octetStreamData$ = this.apiService.post(request)
                            .pipe(map(data => this.octetStreamUrl(data)));
  }
}
