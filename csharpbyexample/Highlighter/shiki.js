import {codeToHtml} from 'https://esm.sh/shiki@1.0.0'
async function Highlight(code) {
    const html = await codeToHtml(code, {
        lang: 'csharp',
        theme: 'vitesse-dark'
    })
    return html;
}