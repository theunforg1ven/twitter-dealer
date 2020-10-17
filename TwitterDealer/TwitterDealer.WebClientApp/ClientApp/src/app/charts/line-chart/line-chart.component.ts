import { Component, Input, OnInit } from '@angular/core';

const LINE_CHART_SAMPLE_DATA: any[] = [
  { data: [32, 14, 40, 23, 38, 48, 43], label: 'User friends'},
  { data: [12, 18, 23, 13, 28, 23, 22], label: 'Current user'},
  { data: [52, 34, 49, 53, 68, 62, 64], label: 'Else users'},
];
const LINE_CHART_LABELS: string[] = ['15.10', '16.10', '17.10', '18.10', '19.10', '20.10', '21.10'];

@Component({
  selector: 'app-line-chart',
  templateUrl: './line-chart.component.html',
  styleUrls: ['./line-chart.component.scss']
})
export class LineChartComponent implements OnInit {
  @Input() inputData: any;

  lineChartData: any = LINE_CHART_SAMPLE_DATA;
  lineChartLabels: any = LINE_CHART_LABELS;
  lineChartOptions: any = {
    responsive: true
  };
  lineChartType = 'line';

  constructor() { }

  ngOnInit(): void {
   if (this.inputData !== null && this.inputData !== undefined){
      this.parseChartData(this.inputData);
    }
  }

  public parseChartData(res: any): void {
    const data = res;
    const allData = data.slice(0);
    console.log(res);

    const formattedValues = allData.reduce((r, e) => {
      r.push([e.placed, e.total]);
      return r;
    }, []);
  }

}
