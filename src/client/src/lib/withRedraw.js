import m from 'mithril';

export const withRedraw = f => {
    f;
    m.redraw();
} 