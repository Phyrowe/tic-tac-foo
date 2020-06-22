import m from 'mithril';

// Deprecated and replaced by redux/redraw middleware
export const withRedraw = f => {
    f;
    m.redraw();
} 