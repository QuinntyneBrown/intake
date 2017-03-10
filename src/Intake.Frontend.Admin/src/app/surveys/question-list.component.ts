import { Question } from "./question.model";
import { QuestionService } from "./question.service";

const template = require("./question-list.component.html");
const styles = require("./question-list.component.scss");

export class QuestionListComponent extends HTMLElement {
    constructor(
		private _document: Document = document,
		private _questionService: QuestionService = QuestionService.Instance) {
        super();
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
		this._bind();
    }

	private async _bind() {
		const questions: Array<Question> = await this._questionService.get();
        for (var i = 0; i < questions.length; i++) {
			let el = this._document.createElement(`ce-question-item`);
			el.setAttribute("entity", JSON.stringify(questions[i]));
			this.appendChild(el);
        }	
	}
}

customElements.define("ce-question-list", QuestionListComponent);
