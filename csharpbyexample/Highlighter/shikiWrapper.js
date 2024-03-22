import {codeToHtml} from 'shiki/bundle/full'

async function Highlight(code) {
    const html = await codeToHtml(code, {
        lang: 'javascript',
        theme: 'vitesse-dark'
    })
    return html;
}